
str [msg] {
    Hello World
} 
,

"once :"  [CurrentDir] { CurrentDir=Directory.GetCurrentDirectory(); } ,

[getopt]{ var opt = stack as Dictionary<String, Func<string,string>>;
    var c = cs.commandLine;
    getopt = (opt.containsKey(c[0]) ?opt[c[0]](c[1]):opt[""](c[0]));  
}

[upload] { upload =  stack + " : toCloud"; }

[downloadFile] { downloadFile = "fromCloud : " + stack; }

{ 
    var options = new Dictionary<String, Func<string,string>>(){
        { [""], (filename) => {return cs.upload(filename);} },
        { ["-get"], (id) => {return cs.downloadFile(id);}} };
    }

    Console.WriteLine(cs.CurrentDir());

        return cs.str.msg + cs.getopt(options);  
}

str [usage] {
usage :    
    pod {filename}
    pod -get {id}

}