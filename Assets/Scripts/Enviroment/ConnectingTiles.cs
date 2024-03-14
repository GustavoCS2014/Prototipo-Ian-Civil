using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Connecting Tile", menuName = "2D/Tiles/Connecting Rule Tile")]
public class ConnectingTiles : RuleTile<ConnectingTiles.Neighbor> {

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int NotNull = 3;
        public const int Null = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.NotNull: return tile is not null;
            case Neighbor.Null: return tile is null;
        }
        return base.RuleMatch(neighbor, tile);
    }
}