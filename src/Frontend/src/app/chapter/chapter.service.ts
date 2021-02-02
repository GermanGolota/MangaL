import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChapter } from '../shared/Models/IChapter';

@Injectable({
  providedIn: 'root'
})
export class ChapterService {
  
 private baseurl = 'https://localhost:5001/api/';
  constructor(private readonly http:HttpClient) { }
  public getChapterBy(chapterId:string):Observable<IChapter>
  {
    let url = this.baseurl +"chapter/"+chapterId;
    return this.http.get<IChapter>(url);
  }
}
