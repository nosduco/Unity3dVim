#!/bin/bash

touch test
#[Configuration]
TERM="xfce4-terminal"

if [ ! -z `vim --serverlist | grep Unity3d` ]; then
    vim --servername Unity3d --remote-silent +$2 $1
else
    $TERM -e "vim --servername Unity3d --remote-silent +$2 $1"
fi
