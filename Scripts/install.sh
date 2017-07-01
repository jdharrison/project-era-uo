# adding mono repository, and installing mono
if [ -z /usr/bin/mono ]; then
    echo "Installing mono ..."
    sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
    echo "deb http://download.mono-project.com/repo/ubuntu xenial main" | sudo tee /etc/apt/sources.list.d/mono-official.list
    sudo apt-get update
    sudo apt-get install mono-complete
else
    echo "Mono installed, skipping ..."
fi

# stop old server, if one is present
if [ -z "/etc/systemd/system/runuo.service" ]; then
    systemctl runuo stop
    echo "Stopping running Server ..."
fi

# copy over the latest service
sudo cp /home/this/project-era-uo/Scripts/runuo.service /etc/systemd/system/runuo.service
sudo systemctl daemon-reload
echo "Installed latest service ..."

echo "Installation done."
