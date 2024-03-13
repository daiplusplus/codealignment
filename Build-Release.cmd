@REM See "appveyor.yml" for these MSBuild commands.

@REM nuget restore CodeAlignment.VisualStudio\packages.config     -PackagesDirectory packages

msbuild code-alignment.sln /t:Restore 

msbuild code-alignment.sln /t:Build /p:Configuration=Release
