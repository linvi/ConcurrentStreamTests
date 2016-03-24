var express = require('express');

var app = express();
var requestId = 0;

var json = {"created_at":"Thu Mar 24 01:37:54 +0000 2016","id":712815673366413313,"id_str":"712815673366413313","text":"Getting tired of trying","source":"\u003ca href=\"http:\/\/twitter.com\/download\/iphone\" rel=\"nofollow\"\u003eTwitter for iPhone\u003c\/a\u003e","truncated":false,"in_reply_to_status_id":null,"in_reply_to_status_id_str":null,"in_reply_to_user_id":null,"in_reply_to_user_id_str":null,"in_reply_to_screen_name":null,"user":{"id":27039368,"id_str":"27039368","name":"Rachael\u2764\ufe0f Riley\u2764\ufe0f","screen_name":"rachaelriley","location":"Portland, CT","url":null,"description":"Followed By Danny Wood12\/30\/12 Donnie Wahlberg1\/11\/13 Vanilla Ice2\/16\/13 BSB 2\/18\/13 Jordan Knight 9\/20\/13 Aaron Carter 5\/3\/14 Todd Carey3\/29\/14","protected":false,"verified":false,"followers_count":2517,"friends_count":2753,"listed_count":24,"favourites_count":17325,"statuses_count":42598,"created_at":"Fri Mar 27 16:30:05 +0000 2009","utc_offset":-14400,"time_zone":"Eastern Time (US & Canada)","geo_enabled":true,"lang":"en","contributors_enabled":false,"is_translator":false,"profile_background_color":"C6E2EE","profile_background_image_url":"http:\/\/pbs.twimg.com\/profile_background_images\/451308874\/429721215.jpg","profile_background_image_url_https":"https:\/\/pbs.twimg.com\/profile_background_images\/451308874\/429721215.jpg","profile_background_tile":false,"profile_link_color":"1F98C7","profile_sidebar_border_color":"C6E2EE","profile_sidebar_fill_color":"DAECF4","profile_text_color":"663B12","profile_use_background_image":true,"profile_image_url":"http:\/\/pbs.twimg.com\/profile_images\/704451853124288512\/oqXKTVHo_normal.jpg","profile_image_url_https":"https:\/\/pbs.twimg.com\/profile_images\/704451853124288512\/oqXKTVHo_normal.jpg","profile_banner_url":"https:\/\/pbs.twimg.com\/profile_banners\/27039368\/1454558735","default_profile":false,"default_profile_image":false,"following":null,"follow_request_sent":null,"notifications":null},"geo":null,"coordinates":null,"place":{"id":"01312c385a78fe53","url":"https:\/\/api.twitter.com\/1.1\/geo\/id\/01312c385a78fe53.json","place_type":"city","name":"Portland","full_name":"Portland, CT","country_code":"US","country":"United States","bounding_box":{"type":"Polygon","coordinates":[[[-72.646719,41.561522],[-72.646719,41.637821],[-72.585258,41.637821],[-72.585258,41.561522]]]},"attributes":{}},"contributors":null,"is_quote_status":false,"retweet_count":0,"favorite_count":0,"entities":{"hashtags":[],"urls":[],"user_mentions":[],"symbols":[]},"favorited":false,"retweeted":false,"filter_level":"low","lang":"en","timestamp_ms":"1458783474662"};

var jsonStr = JSON.stringify(json);
console.log(jsonStr)

var recusive = function(i, end, res) {
    
    console.log('send ' + i + ' to : ' + res.requestId);
    
    res.write(jsonStr + "\r\n");
    
    if (i >= end) {
        res.end();
        return;
    }
    
    if (res.stop) {
        console.log('request was aborted!');
        return;
    }
    
    setTimeout(() => {
        recusive(++i, end, res);
    }, 100);
};

app.get('/stream', function(req, res) {
    req.connection.on('close',function(){    
       res.stop = true;
    });
    
    ++requestId;
    res.requestId = requestId;
    recusive(0, 100, res);
});

app.listen(3000, function() {
    console.log('application started!');
});