#!/bin/bash

echo "Compiling RunUO ..."

cd /var/opt/project-era-uo/RunUO
sudo mcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:NEWTIMERS -d:NEWPARENT -d:MONO -reference:System.Drawing -recurse:Server/*.cs

echo "Compilation done."
