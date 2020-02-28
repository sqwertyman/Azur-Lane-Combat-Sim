using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//slightly altered fleetmenucontroller, specifically for enemy setup. awkwardly done for now
//only uses vanguard lists
public class EnemyMenuController : FleetMenuController
{
    // Start is called before the first frame update
    public override void Start()
    {
        for (int x = 0; x < vanguardShipMenus.Length; x++)
        {
            vanguardShipMenus[x].Init(FleetType.Enemy);

            LoadLoadouts(vanguardShipLoadouts[x], vanguardShipMenus[x], Database.enemyFileName + (x + 1));
        }
    }

    public override bool ExitFleetMenu()
    {
        for (int x = 0; x < vanguardShipMenus.Length; x++)
        {
            SaveLoadout(vanguardShipLoadouts[x], vanguardShipMenus[x], Database.enemyFileName + (x + 1));
        }

        return true;
    }

    protected override bool ShowHideMenus(ShipMenu[] shipMenus)
    {
        return true;
    }
}
