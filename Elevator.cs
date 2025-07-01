using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator_Project
{
    public class Elevator
    {

  
        private bool[] floorReady;
        public int CurrentFloor = 1;
        private int topfloor;
        public ElevatorStatus Status = ElevatorStatus.STOPPED;

        public Elevator(int NumberOfFloors = 10)
        {
            floorReady = new bool[NumberOfFloors + 1];
            topfloor = NumberOfFloors;
        }

        private void Stop(int floor)
        {
            Status = ElevatorStatus.STOPPED;
            CurrentFloor = floor;
            floorReady[floor] = false;
            Console.WriteLine("Stopped at floor {0}", floor);
        }

        private void Descend(int floor)
        {
            for (int i = CurrentFloor; i >= 1; i--)
            {
                if (floorReady[i]) { 
                    Console.WriteLine("Waiting - Descending..");
                    Thread.Sleep(3000); //Simulating Descending
                    Stop(floor);
                }
                else
                    continue;
            }
            Status = ElevatorStatus.STOPPED;
        }

        private void Ascend(int floor)
        {
            for (int i = CurrentFloor; i <= topfloor; i++)
            {
                if (floorReady[i])
                {
                    Console.WriteLine("Waiting - Ascending..");

                    Thread.Sleep(3000); //Simulating Descending
                    Stop(floor);
                }
                else {
                   // ProgressUtility.WriteProgressBar(i, true); 
                   // ProgressUtility.WriteProgress(i, true);
                    continue;
                   
                }
                    
            }

           
            Status = ElevatorStatus.STOPPED;
        }

        void StayPut()
        {
            Console.WriteLine("That's our current floor");
        }

        public void FloorPress(int floor)
        {
            if (floor > topfloor)
            {
                Console.WriteLine("We only have {0} floors", topfloor);
                return;
            }
            if (floor < 0)
            {
                Console.WriteLine("not negative floor allow", floor);
                return;
            }



            floorReady[floor] = true;

            switch (Status)
            {

                case ElevatorStatus.DOWN:
                    Descend(floor);
                    break;

                case ElevatorStatus.STOPPED:
                    if (CurrentFloor < floor)
                        Ascend(floor);
                    else if (CurrentFloor == floor)
                        StayPut();
                    else
                        Descend(floor);
                    break;

                case ElevatorStatus.UP:
                    Ascend(floor);
                    break;

                default:
                    break;
            }


        }

        public enum ElevatorStatus
        {
            UP,
            STOPPED,
            DOWN
        }
    }
}
