import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Subject } from 'rxjs/internal/Subject';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  public metric: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);

  baseUrl: string

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getCurrnetCondition(cityKey: number) {
    return this.http.get<any[]>(this.baseUrl + 'weatherForecast/GetCurrnetCondition?cityKey=' + cityKey);
  }

  getDailyForecasts(cityKey: number) {
    return this.http.get<any>(this.baseUrl + 'weatherForecast/getDailyForecasts?cityKey=' + cityKey + '&metric=' + this.metric.value + '&numDays=' + 5);
  }
}
