
str [msg] {
    Hello World
} 
,

"once :"  [CurrentDir] { CurrentDir=Directory.GetCurrentDirectory(); } ,

{ 
    var opt = new Dictionary<String, object>(){
        [""], (filename) => {cs.upload(filename);},
        ["::"], (id) => {cs.downloadFile(id);}
    }
    Console.WriteLine(cs.CurrentDir());
        return cs.str.msg;  
}