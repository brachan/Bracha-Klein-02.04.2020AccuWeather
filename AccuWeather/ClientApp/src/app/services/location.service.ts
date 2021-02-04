import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  public currentCityCode: BehaviorSubject<number> = new BehaviorSubject<number>(213225);
  baseUrl: string

  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getLocationsAutoComplete(q: string){
    return this.http.get<any[]>(this.baseUrl + 'location?query='+q);
  }
}
