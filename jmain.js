

//# comment for Node JS

import * as std from "std";
import * as os from "os";
import { getRoot } from 'jscript.js'

var loadFile = std.loadFile;

var js=getRoot();

js.commandLine = scriptArgs.slice(2);
try{
    js.WON(loadFile(scriptArgs[1]));
    console.log(js.toString());
}catch(ex){
    if (js.hasOwnProperty("onerror"))console.log(js.onerror(ex));
    else console.log(ex);
}
