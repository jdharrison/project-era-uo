#########################################
## Compile Core RunUO
#########################################

    bash compile.sh

#########################################

#########################################
## Install Service
#########################################

    bash install.sh

#########################################

#########################################
## Managing Server
#########################################

    Use the 'systemctl' command after install.
        
        sudo systemctl enable runuo
        sudo systemctl disable runuo

        sudo systemctl start runuo
        sudo systemctl stop runuo

    Use the 'journalctl' command to view logs.

        sudo journalctl -u runuo

#########################################
