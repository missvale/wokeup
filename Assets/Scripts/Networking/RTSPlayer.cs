using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField] private List<Unit> myUnits = new List<Unit>();

    public List<Unit> GetMyUnits()
    {
        return myUnits;
    }

    #region Server

    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }
    
    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(Unit unit)
    {
        Debug.Log(($"Unit: {unit.connectionToClient.connectionId} | ID : {connectionToClient.connectionId}"));
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
        
        myUnits.Add(unit);
    }   
    
    private void ServerHandleUnitDespawned(Unit unit)
    {
        Debug.Log(($"Unit: {unit.connectionToClient.connectionId} | ID : {connectionToClient.connectionId}"));
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
        
        myUnits.Remove(unit);
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }
    
    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) return;
        
        myUnits.Add(unit);
    }   
    
    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        if (!hasAuthority) return;
        
        myUnits.Remove(unit);
    }

    #endregion

  
}
