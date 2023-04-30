## How to create auto-incrementing file names

A developer may need to create file names that increment e.g. file1.txt, file2.txt etc.

The code presented shows how to in `DirectoryHelpersLibrary.GenerateFiles` For use in your application

- Copy GenerateFiles.cs to your project
- Change property `_baseFileName` to the base file name for creating, currently is set to data.
- Change property `_baseExtension` to the file extension you want.

In the code sample each time the program runs it creates three .json files. 

First time, Data_1.json, Data_2.json and Data_3.json
Second time, Data_4.json, Data_5.json and Data_6.json

And so on.

| Method        |   Description
|:------------- |:-------------
| CreateFile | Generate next file in sequence
| RemoveAllFiles | Removes files generated
| HasAnyFiles | Provides a count of generated files 
| NextFileName | Get next file name without creating the file | 
| GetLast | Get last generated file name | 
