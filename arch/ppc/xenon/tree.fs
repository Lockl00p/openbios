\   xenon specific initialization code
\ 
\   Copyright (C) 2026 Lockl00p
\ 
\   This program is free software; you can redistribute it and/or
\   modify it under the terms of the GNU General Public License
\   as published by the Free Software Foundation
\ 

\ -------------------------------------------------------------
\ device-tree
\ -------------------------------------------------------------

" /" find-device
" device-tree" device-name

" Power Macintosh" model

\ h# 80000000 encode-int " isa-io-base" property


2 encode-int " #address-cells" property
1 encode-int " #size-cells" property
\ Clock frequency???

" AAPL,PowerMac G5" encode-string " MacRISC" encode-string encode+ " compatible" property
" device-tree" encode-string " AAPL,original-name" property
0 encode-int " AAPL,cpu-id" property
" 00000000000000" encode-string " system-id" property
h# 3ef1480 encode-int " clock-frequency" property

new-device
	" memory" device-name
	" memory" device-type
	
	0 h# 1e000000 reg
	external
	: open true ;
	: close ;
finish-device

new-device
	" cpus" device-name
	" cpus" device-type
	1 encode-int " #address-cells" property
	0 encode-int " #size-cells" property

	new-device
		" Xenon,PPE@0" device-name
		" cpu" device-type
		0 encode-int " reg" property
		50000000 encode-int " timebase-frequency" property
		3200000000 encode-int " clock-frequency" property
		h# 8000 encode-int " i-cache-size" property
		h# 80 encode-int " i-cache-line-size" property
		h# 8000 encode-int " d-cache-size" property
		h# 80 encode-int " d-cache-line-size" property
	finish-device
	new-device
		" Xenon,PPE@1" device-name
		" cpu" device-type
		2 encode-int " reg" property
		50000000 encode-int " timebase-frequency" property
		3200000000 encode-int " clock-frequency" property
		h# 8000 encode-int " i-cache-size" property
		h# 80 encode-int " i-cache-line-size" property
		h# 8000 encode-int " d-cache-size" property
		h# 80 encode-int " d-cache-line-size" property
	finish-device

	new-device
		" Xenon,PPE@2" device-name
		" cpu" device-type
		4 encode-int " reg" property
		50000000 encode-int " timebase-frequency" property
		3200000000 encode-int " clock-frequency" property
		h# 8000 encode-int " i-cache-size" property
		h# 80 encode-int " i-cache-line-size" property
		h# 8000 encode-int " d-cache-size" property
		h# 80 encode-int " d-cache-line-size" property
	finish-device

finish-device

" /pci" find-device
	" xenon" model
	h# 02000000 encode-int 0 encode-int encode+ h# 80000000 encode-int encode+ h# 00000200 encode-int encode+
		h# 80000000 encode-int encode+ h# 80000000 encode-int encode+ h# 02000000 encode-int encode+ 0 encode-int encode+
		0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 20000000 encode-int encode+
	" ranges" property
	3 encode-int " #address-cells" property

	h# f900 encode-int 0 encode-int encode+ 0 encode-int encode+ 
	  0 encode-int encode+
	  " interrupt-map-mask" property
	1 encode-int " #interrupt-cells" property
	1 encode-int " #size-cells" property

	h# 40000 " interrupt-parent" property
	

	\ The interrupt map
	\ XMA
		0 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 40 encode-int encode+
	\ SATA CDROM
		h# 0800 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 24 encode-int encode+
	\ SATA disk
		h# 1000 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 20 encode-int encode+
	\ USB OHCI 1
		h# 2000 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 2c encode-int encode+
	\ USb EHCI 1
		h# 2200 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 30 encode-int encode+
	\ USB OHCI 2
		h# 2800 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 34 encode-int encode+
	\ USB EHCI 2
		h# 2900 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 38 encode-int encode+
	\ ethernet
		h# 3800 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 4c encode-int encode+
	\ Flash
		h# 4000 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 18 encode-int encode+
	\ audio out
		h# 4800 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 44 encode-int encode+
	\ SMM, GPIO, etc
		h# 5000 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 14 encode-int encode+
	\ Xenos
		h# 7800 encode-int 0 encode-int encode+ 0 encode-int encode+ 0 encode-int encode+ h# 40000 encode-int encode+ h# 58 encode-int encode+
	" interrupt-map" property
	
	h# 3ef1480 encode-int " clock-frequency" property
	" " encode-string " primary-bridge" property
	0 encode-int " pci-bridge-number" property
	
	0 encode-int 0 encode-int encode+ " bus-range" property

new-device
  " isa" device-name
  " isa" device-type
	2 encode-int " #address-cells" property
	1 encode-int " #size-cells" property

  external
  : open true ;
  : close ;

finish-device

: ?devalias ( alias-str alias-len device-str device-len --
  \		alias-str alias-len false | true )
  active-package >r
  " /aliases" find-device
  \ 2dup ." Checking " type
  2dup find-dev if     \ check if device exists
    drop
    2over find-dev if  \ do we already have an alias?
      \ ." alias exists" cr
      drop 2drop false
    else
      \ ." device exists" cr
      encode-string
      2swap property
      true
    then
  else
    \ ." device doesn't exist" cr
    2drop false
  then
  r> active-package!
  ;

:noname
  " hd"
  " /pci/pci-ata/ata-1/disk@0" ?devalias not if
    " /pci/pci-ata/ata-1/disk@1" ?devalias not if
      " /pci/pci-ata/ata-2/disk@0" ?devalias not if
        " /pci/pci-ata/ata-2/disk@1" ?devalias not if
	  2drop ." No disk found." cr
	then
      then
    then
  then

  " cdrom"
  " /pci/pci-ata/ata-1/cdrom@0" ?devalias not if
    " /pci/pci-ata/ata-1/cdrom@1" ?devalias not if
      " /pci/pci-ata/ata-2/cdrom@0" ?devalias not if
        " /pci/pci-ata/ata-2/cdrom@1" ?devalias not if
	  2drop ." No cdrom found" cr
	then
      then
    then
  then
; SYSTEM-initializer



new-device
	" interrupt-controller" device-name
	" interrupt-controller" device-type
	" 8259" model
	" " encode-string " interrupt-controller" property
	0 encode-int " #address-cells" property
	1 encode-int " #interrupt-cells" property
	h# 00000200 encode-int h# 00050000 encode-int encode+ h# 6000 encode-int encode+ " reg" property

	\ interrupts
	h# 7c encode-int h# 78 encode-int encode+ h# 74 encode-int encode+ h# 70 encode-int encode+ h# 6c encode-int encode+ h# 68 encode-int encode+ h# 64 encode-int encode+ h# 60 encode-int encode+
	h# 5c encode-int encode+ h# 58 encode-int encode+ h# 54 encode-int encode+ h# 50 encode-int encode+ h# 4c encode-int encode+ h# 48 encode-int encode+ h# 44 encode-int encode+ h# 40 encode-int encode+
	h# 3c encode-int encode+ h# 38 encode-int encode+ h# 34 encode-int encode+ h# 30 encode-int encode+ h# 2c encode-int encode+ h# 28 encode-int encode+ h# 24 encode-int encode+ h# 20 encode-int encode+
	h# 1c encode-int encode+ h# 18 encode-int encode+ h# 14 encode-int encode+ h# 10 encode-int encode+ h# 0c encode-int encode+ h# 08 encode-int encode+ h# 04 encode-int encode+
	" interrupts" property
	
finish-device

" /pci" find-device
	" /pci/isa/interrupt-controller" find-dev if 
		encode-int " interrupt-parent" property 
	then
	h# 3800 encode-int 0 encode-int encode+ 
	  0 encode-int encode+ 1 encode-int encode+
	  " /pci/isa/interrupt-controller" find-dev if 
		encode-int encode+
	  then
	  h# 0C encode-int encode+ 1 encode-int encode+
	  " interrupt-map" property

" /pci/isa" find-device
	" /pci/isa/interrupt-controller" find-dev if 
		encode-int " interrupt-parent" property 
	then

\ -------------------------------------------------------------
\ /packages
\ -------------------------------------------------------------

" /packages" find-device

	" packages" device-name
	external
	\ allow packages to be opened with open-dev
	: open true ;
	: close ;

\ /packages/terminal-emulator
new-device
	" terminal-emulator" device-name
	external
	: open true ;
	: close ;
	\ : write ( addr len -- actual )
	\	dup -rot type
	\ ;
finish-device

\ -------------------------------------------------------------
\ The END
\ -------------------------------------------------------------
device-end
