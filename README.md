# Data Mapper for dotnet

The **data-mapper-dotnet** allows developers to map one schema to another by defining a mapping file that can be changed by non-technical staff to meet future needs.

## Features

The data mapper can be used as part of an interface between two IT systems.

![System Context](./docs/l1-system-context.dio.svg)

The developer can focus on implementing the interface while the project manager can define the mapping in a json file.
This way the interface can easily be adjusted if by a project manager without the need of a developer.

For this the developer can:

- Specify an input format (JSON, CSV, etc.)

And the non-technical staff can:

- Specify the mapping from source to target system

## How to use

There are two parts to the library to use it. One is the technical implementation for the developer and one is the mapping for the non-technical staff.

### Library

- TODO

### Mapping

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
      "bpmnr",
      "clientZwTxt"
    ],
    "field-combination": "AND" // Concatenates the source fields with a ", " in between.
  },
  {
    "target-field": "Description",
    "source-fields": [
      "post1Ff"
    ],
    "disabled": true // Mappings that aren't used at the moment can be disabled
  },
  {
    "target-field": "AssignedTo",
    "source-fields": [
      "paraeEmail",
      "parprEmail"
    ],
    "field-combination": "OR" // Takes the first field that isn't empty.
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
    "azure-devops-field": "Project Execution Status",
    "bpm-fields": [
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

#### Functions

A mapping function defines how the `source-fields` are mapped to the `target-field`.
The following mapping functions are available.

- `AND` - Default
  - Concatenates the list of `source-fields` into a single string. The `source-fields` will be concatenated with a `", "` (comma + space).
- `OR`
  - Only the first of the `source-fields` that has a value will be mapped to the `target-field`.

#### Types

A field mapping can define a specific type that the field should have in the target system.
The following types are supported as of now:

- `NUMBER` - Any value that can be parsed into a float
- `Date`
- `Text`

#### Conditions

A condition contains the following information:

| Field    | Values                                        | Description                                         |
| -------- | --------------------------------------------- | --------------------------------------------------- |
| `type`   | `VALUE_EQUALS`<br>`VALUE_EXISTS`<br>`DEFAULT` | Type of the condition to check.                     |
| `field`  |                                               | Field to check the condition on from source system. |
| `equals` | Regex                                         | Only required when the type `VALUE_EQUALS` is set.  |
| `value`  |                                               | The value to set in the field of the target system. |

## Contribute

- TODO
