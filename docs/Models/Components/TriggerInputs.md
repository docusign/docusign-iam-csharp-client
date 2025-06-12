# TriggerInputs

A key-value pair where the key is the `field_name` defined in the `trigger_input_schema` of the workflow definition,
and the value is the actual input data. Supported types include string, number, boolean, object, or array.



## Supported Types

### Str

```csharp
TriggerInputs.CreateStr(/* values here */);
```

### Number

```csharp
TriggerInputs.CreateNumber(/* values here */);
```

### Boolean

```csharp
TriggerInputs.CreateBoolean(/* values here */);
```

### MapOfAny

```csharp
TriggerInputs.CreateMapOfAny(/* values here */);
```

### ArrayOfAny

```csharp
TriggerInputs.CreateArrayOfAny(/* values here */);
```
