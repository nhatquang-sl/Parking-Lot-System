import { Component } from "@angular/core";
import { ParkingClient, ParkCommand, VehicleType } from "../app.api";

@Component({
  selector: "app-vehicle-park",
  templateUrl: "./vehicle-park.component.html",
})
export class VehicleParkComponent {
  public client: ParkingClient;
  public vehicleTypes: any[] = [
    {
      value: VehicleType.Motorbike,
      text: "Motorbike",
    },
    {
      value: VehicleType.Car,
      text: "Car",
    },
  ];
  public successMessage: string = "";
  public errorMessage: string = "";
  public command: ParkCommand;

  constructor(client: ParkingClient) {
    this.client = client;
    this.command = new ParkCommand();
    this.command.licensePlate = "";
    this.command.type = VehicleType.Motorbike;
  }

  public submit() {
    if (this.command.licensePlate === "") {
      alert("This License Plate is required!");
      return;
    }

    this.client.park(this.command).subscribe(
      (result) => {
        this.successMessage = `Your ${
          this.command.type == VehicleType.Motorbike ? "Motorbike" : "Car"
        } parking success at Level: ${result[0].level}, Row: ${
          result[0].row
        }, Spot:`;
        result.forEach((spot) => {
          this.errorMessage = "";
          this.successMessage += ` [${spot.number}]`;
        });
      },
      (error) => {
        this.errorMessage = error.response;
        this.successMessage = "";
      }
    );
  }
}
