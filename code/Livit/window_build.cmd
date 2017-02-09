cd src
for /d %%d in ("Livit.*") do cd %%d & call window_build.cmd & cd .. 
cd ..