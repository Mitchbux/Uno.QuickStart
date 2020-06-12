
str [msg] {
    Trying to work with :
    
}
,

"once :"  
[CurrentDir] { CurrentDir=os.getcwd(); CurrentDir.pop(this); } 

[getopt] {    

    var opt = {
        "": (filename) => {return js.upload(filename);},
        "-get": (id) => {return js.downloadFile(id);}
    }

    var c = js.commandLine;
    getopt = (opt.hasOwnProperty(c[0]) ?opt[c[0]](c[1]):opt[""](c[0]));  
}

[upload] { upload =  stack + " : toCloud"; }

[downloadFile] { downloadFile = "fromCloud : " + stack; }


{try{

    console.log(js.CurrentDir());

    return js.str.msg + js.getopt();

}catch(ex){console.log(js.str.usage());}}

str [usage] {
usage :    
    pod {filename}
    pod -get {id}

}