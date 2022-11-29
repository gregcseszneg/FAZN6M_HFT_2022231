using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {

        IMusicianLogic mLogic;
        ITrackLogic tLogic;
        public QueryController(IMusicianLogic mLogic, ITrackLogic tLogic)
        {
            this.mLogic = mLogic;
            this.tLogic = tLogic;
        }
        [HttpGet("{name}")]
        public IEnumerable<Musician> MusiciansFromRecordLabel(string name)
        {
            return mLogic.MusiciansFromRecordLabel(name);
        }
        [HttpGet]
        public IEnumerable<AvgAgeInRecordLabel> MusicianAverageAgeInTheRecordLabels()
        {
            return mLogic.MusicianAverageAgeInTheRecordLabels();
        }
        [HttpGet]
        public IEnumerable<SumOfMusicLength> SumOfMusicLengthPerMusician()
        {
            return tLogic.SumOfMusicLengthPerMusician();
        }
        [HttpGet("{year}")]
        public IEnumerable<Track> TracksFromMusicianBornAfter(string year)
        {
            return tLogic.TracksFromMusicianBornAfter(year);
        }
        [HttpGet("{length}")]
        public IEnumerable<Musician> MusiciansWhoHasLongerSongThan(string length)
        {
            return tLogic.MusiciansWHoHasLongerSongThan(length);
        }
    }
}
