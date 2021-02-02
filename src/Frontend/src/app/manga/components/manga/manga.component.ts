import { Component, Input, OnInit } from '@angular/core';
import { IManga } from 'src/app/shared/Models/IManga';
import { MangaService } from '../../manga.service';

@Component({
  selector: 'app-manga',
  templateUrl: './manga.component.html',
  styleUrls: ['./manga.component.scss']
})
export class MangaComponent implements OnInit {

  @Input() mangaId:string;
  manga:IManga;
  
  constructor(private readonly service: MangaService) { }

  ngOnInit(): void {
    this.service.getMangaById(this.mangaId).subscribe(result=>{
      this.manga = result;
    }, error=>{
      alert(error);
    })
  }

}
