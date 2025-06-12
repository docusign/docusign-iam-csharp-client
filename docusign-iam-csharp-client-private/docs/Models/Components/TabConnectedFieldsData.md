# TabConnectedFieldsData


## Fields

| Field                                                               | Type                                                                | Required                                                            | Description                                                         |
| ------------------------------------------------------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------- |
| `TypeSystemNamespace`                                               | *string*                                                            | :heavy_check_mark:                                                  | The fully qualified namespace for the type system being verified.   |
| `TypeName`                                                          | *string*                                                            | :heavy_check_mark:                                                  | Name of the type being verified.                                    |
| `SupportedOperation`                                                | [SupportedOperation](../../Models/Components/SupportedOperation.md) | :heavy_check_mark:                                                  | The operation that the field supports.                              |
| `PropertyName`                                                      | *string*                                                            | :heavy_check_mark:                                                  | The name of the individual field being verified.                    |
| `SupportedUri`                                                      | *string*                                                            | :heavy_check_mark:                                                  | Indicates the type verification url of the field.                   |