After updating the *fo-dicom* package via *NuGet* in for example your web application, you may encounter a reflection type load exception where the loader error property of that exception indicates that:
> Dicom.dll version 1.0.0.xx couldn't be found.

This is normally a consequence of failing *assembly binding redirect*. The *assembly binding redirect* should ideally be automatically performed during *NuGet* package installation; the *NuGet* package manager will normally run the `Add-BindingRedirect` command for this purpose.

If the assembly binding redirect does fail, you may either run this PowerShell command manually in the *NuGet* package manager console:

    Get-Project –All | Add-BindingRedirect

which should update all _*.config_ files in your VS solution with the correct redirect data, or you may add a section similar to the following to the the *app.config* (for desktop applications) or *web.config* (for web applications) file:

    <dependentAssembly>
        <assemblyIdentity name="Dicom" publicKeyToken="3a13f649e28eb09a" />
        <bindingRedirect oldVersion="0.0.0.0-1.1.0.0" newVersion="1.1.0.0"/>
    </dependentAssembly>

You can find more information on assembly binding redirect here:

* [Package Manager Console Powershell Reference](https://docs.nuget.org/consume/package-manager-console-powershell-reference)
* [NuGet versioning Part 3: unification via binding redirects](http://blog.davidebbo.com/2011/01/nuget-versioning-part-3-unification-via.html)
* [Updating Assembly Redirects with NuGet](http://weblog.west-wind.com/posts/2014/Nov/29/Updating-Assembly-Redirects-with-NuGet)
* [Could not load file or assembly… NuGet Assembly Redirects](http://blog.maartenballiauw.be/post/2014/11/27/Could-not-load-file-or-assembly-NuGet-Assembly-Redirects.aspx)
