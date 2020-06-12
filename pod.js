
str [msg] {
    Trying to work with :

},"once :"  

[CurrentDir] { CurrentDir=os.getcwd(); CurrentDir.pop(this); } 

[getopt] {    
var opt = stack;
var c = js.commandLine;

    getopt = (opt.hasOwnProperty(c[0]) ?opt[c[0]](c[1]):opt[""](c[0])); }

[upload] { upload =  stack + " : toCloud"; }

[downloadFile] { downloadFile = "fromCloud : " + stack; }


{
    var options =  {
        "": (filename) => {return js.upload(filename);},
        "-get": (id) => {return js.downloadFile(id);}
    };

    console.log(js.CurrentDir());

    return js.str.msg + js.getopt(options);

}

[onerror] { return js.str.usage;}

str [usage] {
usage :    
    pod {filename}
    pod -get {id}

}