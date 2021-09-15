import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { ParkingSpotComponent } from "./parking-spot/parking-spot.component";
import { VehicleParkComponent } from "./vehicle-park/vehicle-park.component";
import { VehicleLeaveComponent } from "./vehicle-leave/vehicle-leave.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ParkingSpotComponent,
    VehicleParkComponent,
    VehicleLeaveComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: ParkingSpotComponent, pathMatch: "full" },
      { path: "vehicle-park", component: VehicleParkComponent },
      { path: "vehicle-leave", component: VehicleLeaveComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
