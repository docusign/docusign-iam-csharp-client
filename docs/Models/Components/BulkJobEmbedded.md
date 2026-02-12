# BulkJobEmbedded

Contains detailed information about the BulkJob including presigned upload links, document IDs, etc


## Fields

| Field                                                                         | Type                                                                          | Required                                                                      | Description                                                                   | Example                                                                       |
| ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| `DocumentStatusEnum`                                                          | List<*string*>                                                                | :heavy_minus_sign:                                                            | All possible document status values                                           | [<br/>"NOT_STARTED",<br/>"IN_PROGRESS",<br/>"CANCELED",<br/>"SUCCEEDED",<br/>"FAILED"<br/>] |
| `Documents`                                                                   | List<[BulkJobEmbeddedItems](../../Models/Components/BulkJobEmbeddedItems.md)> | :heavy_minus_sign:                                                            | List of documents with presigned upload URLs                                  |                                                                               |