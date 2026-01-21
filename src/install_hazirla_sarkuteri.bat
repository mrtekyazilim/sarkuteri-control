
xcopy .\SarkuteriBizerba\bin\Debug\*.dll C:\product_setups\SarkuteriBizerba /a/d/y
xcopy .\SarkuteriBizerba\bin\Debug\*.exe C:\product_setups\SarkuteriBizerba /a/d/y
xcopy .\SarkuteriBizerba\bin\Debug\*.exe.config C:\product_setups\SarkuteriBizerba /a/d/y


xcopy .\SarkuteriAdmin\bin\Debug\*.dll C:\product_setups\SarkuteriAdmin /a/d/y
xcopy .\SarkuteriAdmin\bin\Debug\*.exe C:\product_setups\SarkuteriAdmin /a/d/y
xcopy .\SarkuteriAdmin\bin\Debug\*.exe.config C:\product_setups\SarkuteriAdmin /a/d/y



del C:\product_setups\SarkuteriBizerba.zip

del C:\product_setups\SarkuteriAdmin.zip

c:\portable_programs\7zip\7z.exe a C:\product_setups\SarkuteriAdmin.zip C:\product_setups\SarkuteriAdmin

c:\portable_programs\7zip\7z.exe a C:\product_setups\SarkuteriBizerba.zip C:\product_setups\SarkuteriBizerba