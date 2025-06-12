# Tab


## Fields

| Field                                                                 | Type                                                                  | Required                                                              | Description                                                           |
| --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- |
| `ExtensionData`                                                       | [TabExtensionData](../../Models/Components/TabExtensionData.md)       | :heavy_check_mark:                                                    | N/A                                                                   |
| `TabType`                                                             | *string*                                                              | :heavy_check_mark:                                                    | Indicates the type of tab                                             |
| `ValidationPattern`                                                   | *string*                                                              | :heavy_minus_sign:                                                    | A regular expression used to validate input for the tab.              |
| `ValidationMessage`                                                   | *string*                                                              | :heavy_minus_sign:                                                    | The message displayed if the custom tab fails input validation        |
| `TabLabel`                                                            | *string*                                                              | :heavy_check_mark:                                                    | The label associated to a verification field in a document.           |
| `Radios`                                                              | List<*string*>                                                        | :heavy_minus_sign:                                                    | The radio button properties for the tab (if the tab is of radio type) |