import { Component, OnInit } from '@angular/core';
import { IManga } from 'src/app/shared/Models/IManga';
import { MangaService } from '../../manga.service';

@Component({
  selector: 'app-random-manga',
  templateUrl: '../manga/manga.component.html',
  styleUrls: ['../manga/manga.component.scss']
})
export class RandomMangaComponent implements OnInit {

  manga:IManga;

  constructor(private readonly service:MangaService) { }

  ngOnInit(): void {
    this.service.getRandomManga().subscribe(
      result=>{
        this.manga = result;
        console.log(result);
      },
      error=>{
        console.log(error);
      }
    )
  }
}
