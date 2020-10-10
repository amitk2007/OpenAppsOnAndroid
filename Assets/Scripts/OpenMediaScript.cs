using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#region Notes
/*
fb://page/PAGEIDNUMBER  --https://m.facebook.com/mypage
instagram://user?username=USERNAME --app    --camera    --media?id= --user?username= --location?id= --tag?name= --https://developers.facebook.com/docs/instagram/sharing-to-feed/
tumblr://x-callback-url/blog?blogName=BLOGNAME
twitter://user?user_id=USERID
youtu.be.com/user/USERNAME
 twitter://user?user_id=id

*/
#endregion

public enum mediaApplication
{
    URLLink,
    #region facebook
    /// www.facebook.com

    facebookPage,
    facebookPost,
    facebookProfile,
    facebookGroup,
    #endregion
    #region instagram
    /// www.instagram.com

    instagramProfile,
    instagramTag,
    instagramPost,
    instagramStory,
    #endregion
    #region YouTube
    /// http://youtube.com/

    youtubeVideo,
    youtubeChannelURL = 29,
    youtubeChannel, //https://support.google.com/youtube/answer/2657968?hl=en
    #endregion
    #region tiktok
    /// www.tiktok.com

    tiktokPost,
    tiktokTag,
    tiktokProfile,
    tiktokSound,
    #endregion
    #region discord
    /// discord.com/channels/...

    discordDirect,
    discordGroup,
    #endregion
    #region whatsapp
    /// https://api.whatsapp.com/send?phone=0543455369&text=helo https://web.whatsapp.com/

    whatsappDirectQR,
    whatsappGroupQR,
    whatsappDirectURL,
    whatsappGroupURL,
    #endregion
    #region GitHub
    /// https://github.com/

    githubProfile,
    githubProject,
    #endregion

    /// www.reddit.com www.reddit.com/r/....

    redditPost,

    /// 9gag.com/gag/

    _9gagPost,

    /// https://www.netflix.com/ https://www.netflix.com/watch/......

    netflixShow,


    //Paintrest
    /// <summary>
    /// https://www.tumblr.com/
    /// </summary>
    tumblr,
    //user
    //post
    /// <summary>
    /// twitter.com
    /// </summary>
    twitter,
    //user
    //post
    /// <summary>
    /// web.telegram.org
    /// </summary>
    telegram,
    //message
    //group
}

public class OpenMediaScript : MonoBehaviour
{
    public static Dictionary<mediaApplication, string> linksDictionary;
    public LinkData data;

    private void Start()
    {
        SetDictinaryOfLinks();
    }
    public void SetDictinaryOfLinks()
    {
        //                              Media App & Methos,   URL
        linksDictionary = new Dictionary<mediaApplication, string>();

        linksDictionary.Add(mediaApplication.URLLink, "");
        #region Simple Apps (One/Two links per app)
        //YouTube
        linksDictionary.Add(mediaApplication.youtubeChannel, "https://youtube.com/c/");
        linksDictionary.Add(mediaApplication.youtubeVideo, "https://youtu.be/");
        linksDictionary.Add(mediaApplication.youtubeChannelURL, "https://www.youtube.com/channel/");
        //Discord
        linksDictionary.Add(mediaApplication.discordDirect, "https://discord.gg/channels/@me/");
        linksDictionary.Add(mediaApplication.discordGroup, "https://discord.gg/");
        //9gag
        linksDictionary.Add(mediaApplication._9gagPost, "https://9gag.com/gag/");
        //Netflix
        linksDictionary.Add(mediaApplication.netflixShow, "https://www.netflix.com/title/");
        //Reddit
        linksDictionary.Add(mediaApplication.redditPost, "https://www.reddit.com/r/");
        #endregion

        #region complicated Apps (more then 2 links per app)
        //Facebook
        linksDictionary.Add(mediaApplication.facebookGroup, "fb://group/");//***//
        linksDictionary.Add(mediaApplication.facebookPage, "fb://page/");
        linksDictionary.Add(mediaApplication.facebookPost, "https://www.facebook.com/");//*fb://post/*// add /?sfnsn=mo to make user log in
        linksDictionary.Add(mediaApplication.facebookProfile, "fb://facewebmodal/f?href=https://www.facebook.com/");
        //Instagram
        linksDictionary.Add(mediaApplication.instagramPost, "instagram://media?id=");
        linksDictionary.Add(mediaApplication.instagramProfile, "instagram://user?username=");
        linksDictionary.Add(mediaApplication.instagramTag, "instagram://tag?name=");
        linksDictionary.Add(mediaApplication.instagramStory, "https://www.instagram.com/s/"); //Not relevent for now // maby if highlight //instagram-stories://
        //TikTok
        linksDictionary.Add(mediaApplication.tiktokPost, "https://vm.tiktok.com/");
        linksDictionary.Add(mediaApplication.tiktokSound, "https://vm.tiktok.com/");
        linksDictionary.Add(mediaApplication.tiktokProfile, "https://vm.tiktok.com/");
        linksDictionary.Add(mediaApplication.tiktokTag, "https://vm.tiktok.com/");
        //WhatsApp
        linksDictionary.Add(mediaApplication.whatsappDirectQR, "https://wa.me/");
        linksDictionary.Add(mediaApplication.whatsappGroupQR, "https://wa.me/qr/");
        linksDictionary.Add(mediaApplication.whatsappDirectURL, "https://api.whatsapp.com/send?phone=");
        linksDictionary.Add(mediaApplication.whatsappGroupURL, "https://chat.whatsapp.com/");
        //GitHub
        linksDictionary.Add(mediaApplication.whatsappGroupURL, "https://github.com/");
        #endregion
    }


    public void OpenAppLink()
    {
        LinkData data = EventSystem.current.currentSelectedGameObject.GetComponent<LinkData>();
        string AppURL = linksDictionary[data.application];

        switch (data.application)
        {
            case mediaApplication.whatsappDirectQR:
                Application.OpenURL(AppURL + data.location + "?text=" + data.message);
                break;
            case mediaApplication.whatsappDirectURL:
                Application.OpenURL(AppURL + data.location + "&text=" + data.message);
                break;
            case mediaApplication.redditPost:
                Application.OpenURL(AppURL + data.location + "/comments/" + data.message);
                break;
            default:
                Application.OpenURL(AppURL + data.location + data.message);
                break;
        }
    }

    //https://wa.me/+972543455365?text=hello
    //https://api.whatsapp.com/send?phone=0543455369&text=helo
    //https://www.reddit.com/r/marvelmemes/comments/j6tl2r


    #region to sort later
    public void OpenGitHubProject()
    {
        data = EventSystem.current.currentSelectedGameObject.GetComponent<LinkData>();
        Application.OpenURL("https://github.com/" + data.location + "/" + data.message);
    }

    public void JustOpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void OpenInstagramUser(string userName)
    {
        Application.OpenURL("instagram://user?user=" + userName);
    }
    public void OpenTikTokUser(string userName)
    {
        OpenLink("https://www.tiktok.com/@" + userName);
    }
    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }
    #endregion


    #region Button
    public void NextButton()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
    public void PreviousButton()
    {
        Application.LoadLevel(Application.loadedLevel - 1);
    }
    #endregion
}
