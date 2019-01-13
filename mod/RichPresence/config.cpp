// #include "BIS_AddonInfo.hpp"

class CfgPatches {
    class RichPresence_Scripts {
        units[] = {};
        weapons[] = {};
        requiredVersion = 0.1;
        requiredAddons[] = {
            "DZ_Data"
        };
    };
};

class CfgMods {
    class RichPresence {
        dir = "Rich Presence";
        picture = "";
        action = "";
        hideName = 1;
        hidePicture = 1;
        name = "Discord Rich Presence";
        credits = "Kreyu";
        author = "Kreyu";
        authorID = "0";
        version = "1.0";
        extra = 0;
        type = "mod";

        dependencies[] = { "World", "Mission" };

        class defs {
            class worldScriptModule {
                value = "";
                files[] = { "RichPresence/scripts/4_World" };
            };
            class missionScriptModule {
                value = "";
                files[] = { "RichPresence/scripts/5_Mission" };
            };
        }
    };
};
