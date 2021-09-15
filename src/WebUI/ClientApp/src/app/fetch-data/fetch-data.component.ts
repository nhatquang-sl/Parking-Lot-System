import { Component } from "@angular/core";
import { WeatherForecastClient, WeatherForecast } from "../app.api";

@Component({
  selector: "app-fetch-data",
  templateUrl: "./fetch-data.component.html",
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(client: WeatherForecastClient) {
    client.get().subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => console.error(error)
    );
  }
}