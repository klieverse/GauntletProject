using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

public class AssetManager
{
    protected ContentManager contentManager;

    public AssetManager(ContentManager content)
    {
        contentManager = content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
              return contentManager.Load<Texture2D>(assetName);
    }

    public void PlaySound(string assetName, float position)
    {
        float panPosition= ((position - Camera.Position.X)/GameEnvironment.Screen.X )*2-1;
        SoundEffect snd = contentManager.Load<SoundEffect>("sounds/"+assetName);
        if (panPosition < -1 && panPosition > 1)
            snd.Play(GameEnvironment.Volume,0,panPosition);
        else
        {
            snd.Play(GameEnvironment.Volume, 0, 0);
        }
    }

    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentManager.Load<Song>("sounds/"+assetName));
        MediaPlayer.Volume = GameEnvironment.Volume;
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }
}
