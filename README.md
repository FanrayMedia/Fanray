<p align="center">
  <a href="https://www.fanray.com/">
    <img src="https://user-images.githubusercontent.com/633119/45599313-0d112980-b99e-11e8-9997-d2fcff65347f.png" alt="" width=72 height=72>
  </a>
  <p align="center">
	<a href="#screenshots">Screenshots</a> •
	<a href="#features">Features</a> •
	<a href="#quick-start">Quick Start</a> •
	<a href="#contribute">Contribute</a> •
	<a href="#license">License</a>
  </p>
  <p align="center">
	<a href="https://ci.appveyor.com/project/FanrayMedia/fanray">
	  <img src="https://ci.appveyor.com/api/projects/status/github/fanraymedia/fanray?svg=true" alt="AppVeyor">
	</a>
	<a href="https://fanray.visualstudio.com/Fanray/_build/latest?definitionId=2">
	  <img src="https://fanray.visualstudio.com/Fanray/_apis/build/status/Fanray-CI" alt="Azure Pipelines">
	</a>
    <a href="https://996.icu/#/en_US"><img src="https://img.shields.io/badge/link-996.icu-red.svg" alt="996.icu" /></a>
  </p>
</p>

## Screenshots

<p align="center">
  <img src="https://user-images.githubusercontent.com/633119/54874702-c87fa400-4dad-11e9-86e5-54de38b3319e.png" title="Composer" />
  <img src="https://user-images.githubusercontent.com/633119/54874701-c87fa400-4dad-11e9-8147-1f54ccd0dab4.png" title="Clarity theme" />
</p>

## Features

Fanray is a [blog](https://github.com/FanrayMedia/Fanray/wiki/Use-the-Blog) and web app starter kit. It has an [extensible design](https://github.com/FanrayMedia/Fanray/wiki/Architecture) that allows you to create [plugins](https://github.com/FanrayMedia/Fanray/wiki/Plugins), [themes](https://github.com/FanrayMedia/Fanray/wiki/Themes) and [widgets](https://github.com/FanrayMedia/Fanray/wiki/Widgets). It provides basic infrastructure for building your own web apps on .NET Core. See [wiki](https://github.com/FanrayMedia/Fanray/wiki) for more details.

![Architecture-Extensible-Design](https://user-images.githubusercontent.com/633119/56977708-a7d40800-6b2a-11e9-84ed-c941faadcfa3.png)

| Blog | | Infrastructure |
| --- | --- |  --- | 
| Autosave Draft    | Preferred Domain	| Caching                                   
| Categories		| Responsive Images	| Error Handling
| Comments (Disqus) | RSS				| Events									
| Google Analytics  | SEO-Friendly URLs	| Image Resizing                            
| Media Gallery     | Shortcodes		| Logging (File, Seq, ApplicationInsights)  
| Navigation		| Site Installation	| Middlewares                           
| Open Live Writer  | Tags				| Mini SPAs 
| Pages				| Themes			| Settings                                  
| Plugins			| Users				| Storage (File System, Azure Blob Storage) 
| Posts				| Widgets			| Testing (Unit, Integration)              								
 
## Quick Start

Fanray v1.1 runs on [.NET Core 2.2](https://www.microsoft.com/net/download) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads). Any of the free SQL Server editions, LocalDB, Express, Developer will be sufficient.

Clone the repo then run from either [VS2019](https://www.visualstudio.com/vs/community/) or command line.

- VS2019: open `Fanray.sln`, make sure `Fan.WebApp` is the startup project, Ctrl + F5
- Command line: do the following, then go to https://localhost:5001
 ```
cd <sln folder>
dotnet restore
cd src/Core/Fan.WebApp
dotnet run
```

Database is created for you on app initial launch. Below is the default connection string, to adjust it go to `appsettings.json`

```
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Fanray;Trusted_Connection=True;MultipleActiveResultSets=true"
},
```

The blog setup page will show up on initial launch, simply fill the form out and create your blog.

## Contribute

Please refer to [Contributing Guide](CONTRIBUTING.md).

## Support Me

<a href="https://www.buymeacoffee.com/Fanray" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>

## License

[Apache 2.0](LICENSE)

[![LICENSE](https://img.shields.io/badge/license-Anti%20996-blue.svg)](https://github.com/996icu/996.ICU/blob/master/LICENSE) The [996.icu repo](https://github.com/996icu/996.ICU) is a campaign against the [996 working hour system](https://en.wikipedia.org/wiki/996_working_hour_system) started in Mainland China. I support my fellow software developers over there and elsewhere for this matter on defending their labor rights and improving their labor conditions. As such any entity, whether individual or corporation, that is in violation of the local labor laws against their employees shall not use my repo.