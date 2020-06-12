

//# comment for Node JS

import * as std from "std";
import * as os from "os";
import { getRoot } from 'whole.js'

var writeFile = function(filename,content){var file=std.open(filename,"w");file.puts(content);file.close();};
var loadFile = std.loadFile;

//# uncomment for Node JS

//var fs = require('fs');
//var loadFile = function(filename){return fs.readFileSync(filename, "utf8");};
//var writeFile = function(filename,content){return fs.writeFileSync(filename,content,"utf8");};

//var scriptArgs = process.argv.slice(1);

var js=getRoot();

var commandLine = scriptArgs.slice(2);

js.WON(loadFile(scriptArgs[1]));
console.log(js.toString());
