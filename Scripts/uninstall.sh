if [ -f  "/etc/systemd/runuo.service" ]; then
    echo "Uninstalling server ..."
    systemctl stop runuo
    sudo rm /etc/systemd/runuo.service
else
    echo "Server not installed, skipping ..."
fi

echo "Uninstall done."
