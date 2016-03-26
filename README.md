# IronPython Startup Script Loader for Autodesk Revit and Vasari®
This is a heavily modified and scaled down version of [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) that will load a script named `userSetup.py` at load time. This project has been solely created to accompany the [pyRevit](https://github.com/eirannejad/pyRevit) IronPython script library and provide an independent `userSetup.py` loader for pyRevit.

Please notice that this addin does not provide the python shell. For that you still need to install the RPS since it's the best at what it does and I have no intention of creating another one. This will resolve pyRevit's dependency on the existence of RPS as a host and make it easier to use in a larger environment with users of varied skills that might not need to have access to RPS as a tool but want to use the pyRevit tools thorugh the Revit UI.


## License

## Credits

I'd like to thank people listed here for their great contributions:
  * [Daren Thomas](https://github.com/daren-thomas) (original version, maintainer of [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) for creating RPS that this package is heavily relying on)
  * [Jeremy Tammik](https://github.com/jeremytammik) (creator and maintainer of [RevitLookup](https://github.com/jeremytammik/RevitLookup))

**NOTE**: If you are not on this list, but believe you should be, please contact me!
