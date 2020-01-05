function GenerateClient($swaggerUrl, $apiNamespace, $apiName, $apiHelpersNamespace) {
	$apiHelpersNamespace = "${apiNamespace}.ApiHelpers"
	$clientFileName = "${apiName}Client.cs"
	$clientExtendedFileName = "${apiName}Client.Extended.cs"

	nswag openapi2csclient `
		"/Input:${swaggerUrl}" `
		"/Output:${clientFileName}" `
		"/Namespace:${apiNamespace}.${apiName}Api" `
		"/ClassName:${apiName}Client" `
		"/ClientBaseClass:${apiHelpersNamespace}.ApiClientBase" `
		"/GenerateClientInterfaces:true" `
		"/ConfigurationClass:${apiHelpersNamespace}.ApiConfiguration" `
		"/UseHttpClientCreationMethod:true" `
		"/InjectHttpClient:false" `
		"/UseHttpRequestMessageCreationMethod:false" `
		"/DateType:System.DateTime" `
		"/DateTimeType:System.DateTime" `
		"/GenerateExceptionClasses:false" `
		"/ExceptionClass:${apiHelpersNamespace}.ApiClientException"

	if ($LastExitCode) {
		write-host ""
		write-error "Client generation failed!"
	} else {
		write-host -foregroundcolor green "Updated API client: ${clientFileName}"

		if (-not (test-path $clientExtendedFileName)) {
			write-host ""
			write-host "Please create partial class '${clientExtendedFileName}' with overridden method to supply the service's base address:"
			write-host ""
			write-host -foregroundcolor yellow "`tprotected override Uri GetBaseAddress()"
			write-host ""
		}
	}
}