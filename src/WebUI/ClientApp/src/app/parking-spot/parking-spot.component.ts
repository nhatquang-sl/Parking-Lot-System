import { Component } from "@angular/core";
import { LevelClient, LevelDto, SpotDto } from "../app.api";

@Component({
  selector: "app-parking-spot",
  templateUrl: "./parking-spot.component.html",
})
export class ParkingSpotComponent {
  public levels: LevelDto[];
  public levelSelected: number = 0;
  public spots: SpotDto[];
  public spotWidth: number;

  constructor(client: LevelClient) {
    client.get().subscribe(
      (result) => {
        this.levels = result;
        this.spots = this.levels[this.levelSelected].spots;
        this.spotWidth = 100 / this.filterSpotsOnRow(1).length;
        console.log(new Set(this.spots.map((i) => i.row)));
      },
      (error) => console.error(error)
    );
  }

  public selectLevel(index: number) {
    this.levelSelected = index;
    this.spots = this.levels[this.levelSelected].spots;
  }

  public filterSpots(row: number) {
    var spots = this.filterSpotsOnRow(row);
    var flags = [],
      output = [];
    for (var i = 0; i < spots.length; i++) {
      if (flags[spots[i].vehicleLicensePlate]) continue;
      if (spots[i].vehicleLicensePlate !== null)
        flags[spots[i].vehicleLicensePlate] = true;
      output.push(spots[i]);
    }
    return output;
  }

  public filterSpotsOnRow(row: number) {
    return this.spots
      .filter((x) => x.row == row)
      .sort((a, b) => a.number - b.number);
  }

  public rows() {
    if (this.spots && this.spots.length)
      return new Set(
        this.spots.sort((a, b) => a.row - b.row).map((i) => i.row)
      );
    return [];
  }
}
