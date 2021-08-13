# Validation Service DotNet Core Sample
> A DotNet Core sample service for use the REST validation feature of DocuWare Cloud and DocuWare 6.12 (and above)

Since the release of DocuWare 6.12 it is possible with the document validation to simply verify all index fields before the actual storage to the file cabinet. It's so comfortable to use and flexible for so many scenarios.

Creating dynamic mandatory fields or lookups to external data or to DocuWare before storing is so easy.
The best? The user is being directly notified in the DocuWare webclient in case of wrong entries.

The validation inside DocuWare requires a REST service in order to send all index entries to this.
With this sample application we're going to show you how easily you can setup your own validation webservice.

## How to start with the DotNet Core 3.1 Validation Service project?
As the document validation only requires a REST service we've provided this sample app for DotNet Core 3.1.
It's not required to run your validation service on the same DocuWare server.
You can even host it on platforms like [Microsoft Azure](https://docs.microsoft.com/en-us/dotnet/azure/intro), [Cloud9](https://docs.aws.amazon.com/cloud9/latest/user-guide/sample-dotnetcore.html) or [Google Cloud](https://cloud.google.com/dotnet).

**Make sure that you running the latest version of DocuWare or at least 6.12 for the sample validations.**

1.	Install a C# IDE like Visual Studio 2019 or Visual Studio Code if not already there.
1.	Get latest version of DotNet Core 3.1 https://dotnet.microsoft.com/download/dotnet/3.1
	1.1 When your download completes, run the installer and complete the steps to install .NET on your machine.
	1.2 When the installer completes, open a new command prompt and run the dotnet command. This will verify .NET is correctly installed and ready to use. https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install
2.	Clone the sources: `git clone https://github.com/DocuWare/ValidationService-DotNetCore-Sample.git`
3.	In the command prompt navigate to the folder where the sample app was cloned.
4.  Edit and check [appsettings.json](./src/appsettings.json), all necessary values to be replaced with your own are surrounded with curly brackets.
5.  Open the project with your favor csharp IDE.
6.  Check out [FakeUserService.cs](src/Services/FakeUserService.cs) for the BasicAuthentication username and password.
7.  Start run or debug the project.

Now you receive a message that the validation service was successfully started.

### JSON send by DocuWare to validation service
Data which will be send to the validation service will look like this:
```json
{
	"TimeStamp": "2017-04-07T13:10:44.8888824Z",
	"UserName": "John Doe",
	"OrganizationName": "Peters Engineering",
	"FileCabinetGuid": "8d36692d-e694-4d8c-93db-e97c98897515",
	"DialogGuid": "ac07d242-0120-4575-8722-7c5ae7286026",
	"DialogType": "InfoDialog",
	"Values": [{
		"FieldName": "TEXTFIELD",
		"ItemElementName": "String",
		"Item": "some text"
	},
	{
		"FieldName": "INTFIELD",
		"ItemElementName": "Int",
		"Item": 1234
	},
	{
		"FieldName": "DECIMALFIELD",
		"ItemElementName": "Decimal",
		"Item": 123.45
	},
	{
		"FieldName": "MEMOFIELD",
		"ItemElementName": "Memo",
		"Item": "Long long long long long text"
	},
	{
		"FieldName": "DATEFIELD",
		"ItemElementName": "Date",
		"Item": "2017-04-01T00:00:00Z"
	},
	{
		"FieldName": "DATETIMEFIELD",
		"ItemElementName": "DateTime",
		"Item": "2017-04-02T12:30:00Z"
	},
	{
		"FieldName": "KEYWORDFIELD",
		"ItemElementName": "Keywords",
		"Item": {
			"Keyword": ["keyword1", "keyword2","keyword3"]
		}
	}]
}
```
###### TimeStamp
Time stamp of the request. Datetime in UTC formatted in the [Roundtrip format](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip)

###### UserName
The long name of the DocuWare user that requests the validation.

###### OrganizationName
The name of the organization containing the dialog.

###### FileCabinetGuid
Guid of the file cabinet containing the dialog.

###### DialogGuid
Guid of the dialog for which the validation is made.

###### DialogType
Type of the dialog. Available values are:
* `InfoDialog` - Info dialog for editing index values.
* `Store` - Store dialog for storing new documents in the file cabinet.

###### Values
A list of values to be validated. Each value contains the following elements:
* `FieldName` - the db name of the field.
* `ItemElementName` - string value representing the type of the field. Can be one of the following:
 * `String` - Value element is string in quotation marks. Example: "Some text"
 * `Int` - Value element is Int32 or Int64 formatted in Invariant culture. Example: 1243
 * `Decimal` - Value element is Decimal formatted in Invariant culture. Example: 123.45
 * `Memo` - Value element element is string in quotation marks. "Some long text"
 * `Date` - Date in UTC formatted in the [Roundtrip format](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip) always in midnight. Example: "2017-04-01T00:00:00Z"
 * `DateTime` - Datetime in UTC formatted in the [Roundtrip format](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip). Example: "2017-04-02T12:30:00Z"
 * `Keywords` - Element contains subelement "Keyword" with value array of strings. Example: 
	`{"Keyword": ["keyword1", "keyword2","keyword3"]}`
* `Item` - The value of the field formatted as described above.
<br />

### Expected JSON response format by DocuWare
**_ NOTE: Make sure you always return HTTP status code 200. Because this is used for testing the availability of the validation service! _**

The expected response is JSON with the following structure:
```javascript
// Successful validation
{
	"Status": "OK",
	"Reason": "Everything is fine"
}

// Failed validation
{
	"Status": "Failed",
	"Reason": "Your input is not OK! Check the values!"
}
```

###### Status
The status of the validation. For successful validation the expected value is "OK". Every other value indicates that the validation has failed.

###### Reason (Optional)
Reason for the failed validation. This is the message that is shown to the user.

## Supported DocuWare versions
These sample validations requires an installation of ***DocuWare 6.12*** system or higher.

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are warmly welcome.

## Licensing

*DocuWare Validation Service DotNet Core sample* proudly using the [MIT License](LICENSE).

