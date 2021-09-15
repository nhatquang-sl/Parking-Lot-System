import { Component } from "@angular/core";
import { ParkingClient, LeaveCommand } from "../app.api";

@Component({
  selector: "app-vehicle-leave",
  templateUrl: "./vehicle-leave.component.html",
})
export class VehicleLeaveComponent {
  public client: ParkingClient;
  public command: LeaveCommand;
  public successMessage: string = "";

  constructor(client: ParkingClient) {
    this.client = client;
    this.command = new LeaveCommand();
    this.command.licensePlate = "";
  }

  public submit() {
    if (this.command.licensePlate === "") {
      alert("This License Plate is required!");
      return;
    }

    this.client.leave(this.command).subscribe(
      (result) => {
        console.log(result);
        this.successMessage = `Your leaving success.`;
      },
      (error) => {
        console.error(error);
        console.error(JSON.stringify(error));
      }
    );
  }
}
