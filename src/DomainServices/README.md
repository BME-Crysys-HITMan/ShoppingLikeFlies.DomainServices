# Domain services

## Usage

Add to dependency ijection via

```c#
services.AddCaffProcessor(configuration);

services.AddCaffProcessor( validator=>{
	validator.Validator = "path/to/validator/executable";
	validator.GeneratorDir = "path/to/generated/files";
}, upload => {
	upload.DirectoryPath = "path/to/save/files";
	upload.ShouldUploadToAzure = false;
}, configuration);

```

Then just before configuring pipeline,
Initialize the db by calling

```c#
app.UseDomainServices(isDevelopment: true);
```