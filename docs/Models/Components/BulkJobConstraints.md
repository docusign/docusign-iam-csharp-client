# BulkJobConstraints

Describes the limits of a bulk job, or an action associated with a bulk job


## Fields

| Field                    | Type                     | Required                 | Description              | Example                  |
| ------------------------ | ------------------------ | ------------------------ | ------------------------ | ------------------------ |
| `AllowedFormats`         | List<*string*>           | :heavy_minus_sign:       | N/A                      | [<br/>"pdf",<br/>"docx",<br/>"txt"<br/>] |
| `MaxDocumentsPerJob`     | *int*                    | :heavy_minus_sign:       | N/A                      | 10000                    |
| `MaxSizeMb`              | *int*                    | :heavy_minus_sign:       | N/A                      | 100                      |
| `TimeoutSeconds`         | *int*                    | :heavy_minus_sign:       | N/A                      | 300                      |