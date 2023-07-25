import { Component } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { GameModel } from '../Model/GameModel';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})


export class GamesComponent {

  Games:any;
  singleGame: any;

  constructor( private http:HttpClient){

  };
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };


   ngOnInit() {
     this.getGames();


  }

  addGame(game:any){
    var gameInfo = new GameModel(game.name, game.genre, game.price, game.releaseDate);
    return this.http.post('https://localhost:7207/games', gameInfo).subscribe(data => {
      data = gameInfo;
  });
  }

  deleteGame(game:any){
    return this.http.delete('https://localhost:7207/games/' + game.id).subscribe(data => {
      data = game;
  });
  }

  updateGame(game:any){
    return this.http.put('https://localhost:7207/games/' + game.id, game).subscribe(data => {
      data = game;
  });
  }

   getGames(){
    this.http.get('https://localhost:7207/games').subscribe(data => this.Games = data);
    //return this.http.get('https://localhost:7207/games');
    return this.Games;

  }

   getGame(game:any){
        this.http.get('https://localhost:7207/games/' + game.id).subscribe(data => {this.singleGame = data});
  }

}
