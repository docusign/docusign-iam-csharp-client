# ValidationError

A single field-level or parameter-level validation error.


## Fields

| Field                                                                      | Type                                                                       | Required                                                                   | Description                                                                | Example                                                                    |
| -------------------------------------------------------------------------- | -------------------------------------------------------------------------- | -------------------------------------------------------------------------- | -------------------------------------------------------------------------- | -------------------------------------------------------------------------- |
| `Code`                                                                     | *string*                                                                   | :heavy_check_mark:                                                         | A machine-readable error code identifying the specific validation failure. | invalid_email                                                              |
| `Message`                                                                  | *string*                                                                   | :heavy_check_mark:                                                         | A human-readable description of the validation error.                      | The provided email format is incorrect.                                    |
| `Target`                                                                   | *string*                                                                   | :heavy_minus_sign:                                                         | The name of the field, parameter, or path segment that caused the error.   | email                                                                      |