#!/bin/sh
csc whole.cs /out:whole.exe
exec mono whole.exe pod.cs