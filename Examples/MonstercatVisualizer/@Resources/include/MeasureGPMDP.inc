[MeasureState]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=State
UpdateDivider=10

[MeasureStateButton]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=State
Substitute="0":"Play","1":"Pause","2":"Play"

[MeasureTrack]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Title
Substitute="":"#NoTrackNameText#"
UpdateDivider=100

[MeasureArtist]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Artist
Substitute="":"#NoArtistNameText#"
UpdateDivider=100

[MeasureCover]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Cover
DefaultPath=#@#images\nocover.png
CoverPath=#@#images\coverGPMDP.png
Substitute="":"#@#images\nocover.png"
Paused=#ShowMonstercatCover#
UpdateDivider=50

[MeasureCoverAbs]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=CoverWebAddress
Substitute="":"#@#images\nocover.png"
DynamicVariables=1
UpdateDivider=50

[MeasurePosition]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Position
IncludeMS=1

;Redundant, just here to fix a spotify related bug
[MeasurePositionDec]
Measure=Calc
Formula=MeasurePosition
DynamicVariables=1

[MeasureDuration]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Duration
UpdateDivider=10

[MeasureProgress]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Progress

[MeasureAutoHideGPMDP]
Measure=Plugin
Plugin=GPMDPPlugin
PlayerInfo=Status
IfEqualValue=0
IfEqualAction=[!HideFade][!HideFade "#ROOTCONFIG#\Song Information"]
IfAboveValue=0
IfAboveAction=[!ShowFade][!ShowFade "#ROOTCONFIG#\Song Information"]
Disabled=#DisableAutoHide#
UpdateDivider=100
