export class GameModel
{
    GameName: string;
    GameGenre: string;
    GamePrice: number;
    GameReleaseDate: string;

    constructor(name:string,genre:string,price:number,releaseDate:string){
      this.GameName = name;
      this.GameGenre = genre;
      this.GamePrice = price;
      this.GameReleaseDate = releaseDate;
    }

    toJSON(){
      return{
        name: this.GameName,
        genre: this.GameGenre,
        price: this.GamePrice,
        releaseDate: this.GameReleaseDate,
      }
    }
}
