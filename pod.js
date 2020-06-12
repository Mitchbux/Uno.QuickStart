
str [msg] {
    Hello World
}
,

"once :"  [CurrentDir] { CurrentDir=os.getcwd(); CurrentDir.pop(this); } 
,

{ 
    console.log(js.CurrentDir());
        return js.str.msg;  
}