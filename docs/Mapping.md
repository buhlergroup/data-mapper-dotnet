# Mapping

Here's a sample of how the mapping could look like:

```JSON
[
  {
    "target-field": "ID",
    "source-fields": [
      "nr"
    ],
    "type": "NUMBER"
  },
  {
    "target-field": "Title",
    "source-fields": [
      "nr",
      "clientZwTxt"
    ],
    "field-combination": "AND"
  },
  {
    "target-field": "Description",
    "source-fields": [
      "post1Ff"
    ],
    "disabled": true
  },
  {
    "target-field": "AssignedTo",
    "source-fields": [
      "paraeEmail",
      "parprEmail"
    ],
    "field-combination": "OR"
  },
  {
    "target-field": "Tags",
    "source-fields": [
      "yyxba"
    ]
  },
  {
    "target-field": "State",
    "source-fields": [],
    "conditions": [
        {
        "type": "VALUE_DOES_NOT_EQUAL",
        "field": "ms14ActualFf",
        "equals": "0000-00-00",
        "value": "Closed"
      },
      {
        "type": "VALUE_DOES_NOT_EQUAL",
        "field": "ms6ActualFf",
        "equals": "0000-00-00",
        "value": "In Progress"
      },
      {
        "type": "VALUE_DOES_NOT_EQUAL",
        "field": "ms1ActualFf",
        "equals": "0000-00-00",
        "value": "Started"
      },
    ]
  },
  {
    "target-field": "Project Execution Status",
    "source-fields": [
      "status"
    ],
    "type": "SELECT",
    "select-values": [
      {
        "origin": "CN",
        "destination": "CN - Cancelled"
      },
      {
        "origin": "DO",
        "destination": "DO - Done"
      },
      {
        "origin": "IP",
        "destination": "IP - In Preparation"
      },
      {
        "origin": "ST",
        "destination": "ST - Stopped"
      }
    ]
  },
]
```

## Field Combinations

A mapping combination defines how the `source-fields` are mapped to the `target-field`.
The following mapping functions are available.

- `AND` - Default
  - Concatenates the list of `source-fields` into a single string. The `source-fields` will be concatenated with a `", "` (comma + space).
- `OR`
  - Only the first of the `source-fields` that has a value will be mapped to the `target-field`.

## Types

A field mapping can define a specific type that the field should have in the target system.
The following types are supported as of now:

- `NUMBER` - Any value that can be parsed into a float
- `DATE`
- `TEXT`
- `SELECT` - Maps a specific value to another one

## Conditions

A condition contains the following information:

| Field    | Values                                                           | Description                                                           |
| -------- | ---------------------------------------------------------------- | --------------------------------------------------------------------- |
| `type`   | `VALUE_EQUALS`<br>`VALUE_MATCHES`<br>`VALUE_EXISTS`<br>`DEFAULT` | Type of the condition to check.                                       |
| `field`  |                                                                  | Field to check the condition on from source system.                   |
| `equals` |                                                                  | Only required when the type `VALUE_EQUALS` or `VALUE_MATCHES` is set. |
| `value`  | `<value>`<br>`DO_NOT_OVERRIDE`                                   | The value to set in the field of the target system.                   |

### Type

The following condition types exist:

- `VALUE_EQUALS` - Condition is applied if the value of the filed matches the value specified in the `equals` field (Case insensitive).
- `VALUE_MATCHES` - Condition is applied if the value of the filed matches the regex specified in the `equals` field.
- `VALUE_EXISTS` - Condition is applied if a value exists in the field.
- `DEFAULT` - Condition is always applied

### Value

- `<value>` - Writes the value in the field if the condition applies
- `DO_NOT_OVERRIDE` - Prevents the value from being updated if the condition applies