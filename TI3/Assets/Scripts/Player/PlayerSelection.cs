using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelection : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private RectTransform workerSelectionArea = null;

    [Header("Setted during playtime")]
    [SerializeField] private List<Worker> selectedWorkers = new List<Worker>();
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private bool isMouseOverUI;
    [SerializeField] private bool isDrawing;

    public List<Worker> GetSelectedWorkers()
    {
        return selectedWorkers;
    }
    public void ResetSelectedWorkers()
    {
        selectedWorkers = new List<Worker>();
    }
    public void RemoveSelectedWorker(Worker worker)
    {
        selectedWorkers.Remove(worker);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClearSelectionArea();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateSelectionArea();
        }
        if (IsMouseOverUI() == true) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            StartSelectionArea();
        }
    }
    public bool IsMouseOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            isMouseOverUI = true;
            return true;
        }
        else
        {
            isMouseOverUI = false;
            return false;
        }
    }
    private void StartSelectionArea()
    {
        isDrawing = true;

        if (Input.GetKey(KeyCode.LeftShift) == false) // Se o botão shift não estiver sendo pressionado...
        {
            foreach (Worker selectedWorker in selectedWorkers) // Para todas as unidades selecionadas...
            {
                selectedWorker.BeDeselected(); // Desativa o highlight de todas as unidades selecionadas
            }

            selectedWorkers.Clear(); // Limpa a lista de unidades selecionadas
        }

        workerSelectionArea.gameObject.SetActive(true); // Se o botão shift estiver sendo pressionado, ativa a area de seleção

        startPosition = Input.mousePosition; // Define a posição inicial da area de seleção

        UpdateSelectionArea();
    }

    private void UpdateSelectionArea()
    {
        if (isDrawing == false) { return; }

        Vector2 mousePosition = Input.mousePosition;

        float areaWidth = mousePosition.x - startPosition.x;
        float areaHeight = mousePosition.y - startPosition.y;

        workerSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        workerSelectionArea.anchoredPosition = startPosition + new Vector2(areaWidth / 2, areaHeight / 2);
    }

    private void ClearSelectionArea()
    {
        if (isDrawing == false) { return; }

        isDrawing = false;

        workerSelectionArea.gameObject.SetActive(false); // Desativa a area de seleção

        if (workerSelectionArea.sizeDelta.magnitude == 0) // Se não tiver movido o mouse durante a seleção...
        {
            #region UNUSED_GetUsingRaycast
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // == Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, workerLayerMask) == false) { return; } // Se o raycast pegar nada, retorna

            //if (hit.collider.TryGetComponent<Worker>(out Worker worker) == false) { return; } // Verifica se há o componente Unit. Se não, retorna
            #endregion

            foreach (Worker worker in PlayerSystem.Instance.GetMyWorkers())
            {
                if (selectedWorkers.Contains(worker)) { continue; }

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(worker.transform.position);

                if ((screenPosition - Input.mousePosition).magnitude < 12)
                {
                    selectedWorkers.Add(worker); // Adiciona a unidade à lista
                    worker.BeSelected();
                }
            }

            //foreach (Worker selectedWorker in selectedWorkers) // Para todas as unidades selecionadas...
            //{
            //    selectedWorker.BeSelected(); // Ativa o highlight em todas as unidades selecionadas
            //}

            return;
        }

        Vector2 min = workerSelectionArea.anchoredPosition - (workerSelectionArea.sizeDelta / 2);
        Vector2 max = workerSelectionArea.anchoredPosition + (workerSelectionArea.sizeDelta / 2);

        // Tentar com boxcast depois

        foreach (Worker worker in PlayerSystem.Instance.GetMyWorkers()) // Para todas as unidades selecionadas...
        {
            if (selectedWorkers.Contains(worker)) { continue; } // Se a unidade já existir na lista, vai para a próxima unidade

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worker.transform.position); // Converte a posição no mundo da unidade para a posição na tela da unidade

            if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y) // Se o transform da unidade estiver dentro da área de seleção
            {
                selectedWorkers.Add(worker); // Adiciona a unidade à lista
                worker.BeSelected(); // Ativa o highlight da unidade
            }
        }
    }
}