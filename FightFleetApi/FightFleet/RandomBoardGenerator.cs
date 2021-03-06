﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightFleet
{
    /// <summary>
    /// Randomly places 5 ships to a game board.
    /// </summary>
    public class RandomlyGenerateBoard : GenerateBoard
    {
        public override void Generate() {
            // Place 5 ships on the board
            RandomlyPlaceShip(new AircraftCarrier());
            RandomlyPlaceShip(new BattleShip());
            RandomlyPlaceShip(new Submarine());
            RandomlyPlaceShip(new Cruiser());
            RandomlyPlaceShip(new Destroyer());
        }

        /// <summary>
        /// Randomly places a ship in the board. Random location as well as direction (horizontal, vertical) of the ship
        /// </summary>
        /// <param name="ship"></param>
        public void RandomlyPlaceShip(Ship ship) {
            var random = new Random();

            while (true)
            {
                var randomPosition = new KeyValuePair<int, int>(random.Next(0, GameBoard.XSIZE),
                                                                random.Next(0, GameBoard.YSIZE));

                if (Board.BoardCells[randomPosition.Key, randomPosition.Value] != (int) BoardCellStatus.Blank)
                    continue;

                // try to pick a random direction 6 (can be changed) times. If not successful precede to pick another random position
                for (int i = 0; i <= 5; i++)
                {
                    var randomDirection = random.Next(1, Enum.GetNames(typeof (ShipDirections)).Count() + 1);

                    var shipPositions = ShipCellPositions((ShipDirections) randomDirection, randomPosition, ship.Size);

                    if (shipPositions != null)
                    {
                        MarkBoardWithShipPositions(shipPositions);
                        return;
                    }
                }
            }
        }
    }
}
