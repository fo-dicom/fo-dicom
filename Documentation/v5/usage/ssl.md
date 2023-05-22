## SSL connections

### Server certifiacte

When creating a DicomServer with `DicomServerFactory.Create` there is an overload that takes an instance of `ITlsAcceptor` as parameter.
If an instance is passed, then the DicomServer tries to establish an ssl connection using the method

```csharp
Stream AcceptTls(Stream encryptedStream, string remoteAddress, int localPort);
```

fo-dicom comes with a default implementation of this interface, called `DefaultTlsAcceptor`. This class takes an X509Certificate as parameter. 
Optionally you can set parameters like timeout, whether the client also should authenticate with a certificate, or the .net built in `RemoteCertificateValidationCallback` by which you can handle the validation yourself.
Of course it's recommended to build your own class inheriting from ITlsAcceptor which then contains all your ssl specific logic.

Instead of passing the instance to your DicomServer Factory method, you also can register your implementation of ITlsAcceptor in your DI container.
If not set explicitly as parameter, the Create-method will check the DI ServiceProvider if there is a service of type ITlsAcceptor registered and then takes the instance.
So you can easyly configure or set your server certificate on application startup by configuring the DI services. This is also useful if you want go get some other methods from DI service provider within your ITlsAcceptor implementation (like logger, certificate manager, etc).

### Client certificate

The DicomClient factory method `DicomClientFactory.Create` can take a instance of `ITlsInitiator` as parameter. With that, your client can initiate a ssl connection to a DicomServer with the method

```csharp
Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort)
```

fo-dicom comes with a default implementation of this interface, namely `DefaultTlsInitiator`. This implementation has some properties to control the behavior. For example you can turn off all ssl checks by setting `IgnoreSslProicyError` to `false`. This implementation also has the .net built in `RemoteCertificateValidationCallback` handler.
By setting a X509Certificate to the `DefaultTlsInitiator` you can also tell the client to use a client certifiate for authentication.

If you only want the default behavor and let your operating system do all ssl checks, then you can still call the DicomClientFactory method and just set the `useSsl` property to true, then fo-dicom will automatically create an default instance of the `ITlsInitiator` implementation.

*note*
The unittest, which checks an ssl connection with client certificate on .net462, always failed on github actions, but successfully worked fine on local machines. Obviously in .net framework the certificate handling is more strict than in .net core, because some certificates without X509v3 Extended Key Usage where successfully processed when running in .net core, but failed when running in .net framework.

