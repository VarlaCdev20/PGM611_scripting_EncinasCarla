using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using logica_jugador;

[InitializeOnLoad]
public static class CrearEscenaInicial
{
    const string Escena = "Assets/Scenes/Main.unity";
    const string Fondo = "Assets/LegacyFantasy/Background.png";
    const string Personaje = "Assets/LegacyFantasy/Idle-Sheet.png";
    const string Bloque = "Assets/LegacyFantasy/BloquePasto.png";

    static CrearEscenaInicial()
    {
        EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
        EditorApplication.delayCall += Crear;
    }

    static void Crear()
    {
        if (EditorApplication.isCompiling || EditorApplication.isUpdating)
        {
            EditorApplication.delayCall += Crear;
            return;
        }

        EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;

        if (File.Exists(Escena))
        {
            if (SceneManager.GetActiveScene().path != Escena)
                EditorSceneManager.OpenScene(Escena);

            Vista2D();
            return;
        }

        Directory.CreateDirectory("Assets/Scenes");

        ConfigurarSprite(Fondo, 32);
        ConfigurarSprite(Personaje, 32);
        ConfigurarSprite(Bloque, 16);

        Sprite fondo = CargarPrimerSprite(Fondo);
        Sprite personaje = CargarPrimerSprite(Personaje);
        Sprite bloque = AssetDatabase.LoadAssetAtPath<Sprite>(Bloque);

        if (fondo == null || personaje == null || bloque == null)
        {
            EditorApplication.delayCall += Crear;
            return;
        }

        Scene escena = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Cámara 2D
        GameObject camara = new GameObject("Main Camera");
        camara.tag = "MainCamera";
        camara.transform.position = new Vector3(0, 0, -10);
        Camera camera = camara.AddComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 5;
        camara.AddComponent<AudioListener>();

        // Fondo
        GameObject bg = new GameObject("Fondo");
        SpriteRenderer bgRenderer = bg.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = fondo;
        bgRenderer.sortingOrder = -10;

        // Bloques individuales: cada uno se puede seleccionar y mover por separado.
        GameObject bloques = new GameObject("Bloques");

        Vector2[] posiciones =
        {
            new Vector2(-4, -3), new Vector2(-3, -3), new Vector2(-2, -3),
            new Vector2(-1, -3), new Vector2( 0, -3), new Vector2( 1, -3),
            new Vector2( 3, -2), new Vector2( 4, -2),
            new Vector2(-4,  0), new Vector2(-3,  0),
            new Vector2( 1,  1),
            new Vector2( 4,  2)
        };

        for (int i = 0; i < posiciones.Length; i++)
        {
            GameObject b = new GameObject("Bloque_" + (i + 1));
            b.transform.SetParent(bloques.transform);
            b.transform.position = posiciones[i];

            SpriteRenderer sr = b.AddComponent<SpriteRenderer>();
            sr.sprite = bloque;

            b.AddComponent<BoxCollider2D>();
        }

        // Personaje
        GameObject player = new GameObject("Player");
        player.transform.position = new Vector3(-3, 3, 0);

        SpriteRenderer pr = player.AddComponent<SpriteRenderer>();
        pr.sprite = personaje;
        pr.sortingOrder = 5;

        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        BoxCollider2D col = player.AddComponent<BoxCollider2D>();
        col.size = new Vector2(0.48f, 1.36f);
        col.offset = new Vector2(-0.20f, -0.04f);

        // Script de clase, sin movimiento.
        player.AddComponent<Jugador>();

        EditorSceneManager.SaveScene(escena, Escena);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(Escena, true) };

        Selection.activeGameObject = player;
        Vista2D();

        Debug.Log("Escena básica creada: fondo, personaje y bloques individuales.");
    }

    static void ConfigurarSprite(string ruta, float ppu)
    {
        TextureImporter importer = AssetImporter.GetAtPath(ruta) as TextureImporter;
        if (importer == null) return;

        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = ppu;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.mipmapEnabled = false;
        importer.alphaIsTransparency = true;
        importer.SaveAndReimport();
    }

    static Sprite CargarPrimerSprite(string ruta)
    {
        return AssetDatabase.LoadAllAssetsAtPath(ruta).OfType<Sprite>().FirstOrDefault();
    }

    static void Vista2D()
    {
        foreach (SceneView view in SceneView.sceneViews)
        {
            view.in2DMode = true;
            view.orthographic = true;
            view.Repaint();
        }
    }
}
